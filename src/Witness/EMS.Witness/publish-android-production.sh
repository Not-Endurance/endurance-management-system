while getopts "p:" option; do
  case $option in
    a) alias=$OPTARG;;
    p) pass=$OPTARG;;
    \?) echo "❌ Error: Invalid option"
      exit;;
   esac
done

# Require -a argument
if [ -z "$alias" ]; then
    echo "Keystore Alias is required. Please enter an alias:"
    read alias_input
    alias=$alias_input
fi
# Require -p argument
if [ -z "$pass" ]; then
   echo "Password is required for signing. Please enter a value:"
   read -s -p "Keystore password: " pass
   echo
fi

keystore_path=C:/Work/secrets/Android/EMS.Apps/EMS.Apps.keystore

if keytool -list -keystore "$keystore_path" -alias "$alias" -storepass "$pass" &>/dev/null; then
    echo "✅ Success: Alias '$alias' exists in keystore and password is correct."
else
    echo "❌ Error: Either the alias '$alias' does not exist in the keystore or the password is incorrect."
    read -n 1 -s -r -p "Press any key to continue"
    exit 1
fi

rm -rf bin/$build/$target

target=net8.0-android
build=Release
package_format="aab"

dotnet publish \
 -f "$target" \
 -c "$build" \
 -p:AndroidKeyStore=true \
 -p:AndroidSigningKeyStore="$keystore_path" \
 -p:AndroidSigningKeyAlias="$alias" \
 -p:AndroidSigningKeyPass="$pass" \
 -p:AndroidSigningStorePass="$pass" \
 -p:AndroidPackageFormat="aab" \
 -p:AndroidUseApkSigner=true

if [ $? -eq 1 ]; then
    echo 'publish failed'
else
    cd "bin/$build/$target"
    explorer .
    cd -
fi

find "bin/$build/$target" -name "*.aab" | while read -r aab_path; do
    if [ -z "$aab_path" ]; then
        echo "⚠️ No AAB files found."
        exit 1
    fi

    echo "✍️ Signing: $aab_path"

    jarsigner \
      -verbose \
      -sigalg SHA256withRSA \
      -digestalg SHA-256 \
      -keystore "$keystore_path" \
      -storepass "$pass" \
      -keypass "$pass" \
      "$aab_path" \
      "$alias"

    if [ $? -eq 0 ]; then
        echo "✅ Successfully signed: $aab_path"
    else
        echo "❌ Failed to sign: $aab_path"
        exit 1
    fi

    echo
done

find "bin/$build/$target" -name "*.aab" | while read -r aab_path; do
    if [ -z "$aab_path" ]; then
        echo "⚠️ No AAB files found."
        exit 1
    fi

    echo "🔍 Verifying signing for: $aab_path"

    verify_output=$(jarsigner -verify -verbose -certs "$aab_path" 2>&1)

    if echo "$verify_output" | grep -q "CN=Android Debug"; then
        echo "❌ $aab_path is signed with DEBUG key!"
    elif echo "$verify_output" | grep -q "jar verified"; then
        echo "✅ $aab_path is signed with a custom (release) keystore."
    else
        echo "⚠️ $aab_path signature verification failed or is invalid."
        echo "$verify_output"
    fi

    echo
done