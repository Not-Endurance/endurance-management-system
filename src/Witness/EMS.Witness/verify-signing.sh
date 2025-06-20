target=net8.0-android
build=Release

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