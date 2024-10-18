 target=net8.0-android
 build=Release

dotnet publish \
-f "$target" \
-c "$build" -p:AndroidKeyStore=true -p:AndroidSigningKeyStore="D:/Source/NTS/secrets/Android/EMS.Apps/EMS.Apps.keystore" -p:AndroidSigningKeyAlias=ems.apps -p:AndroidSigningKeyPass=test12 -p:AndroidSigningStorePass=test12

if [ $? -eq 1 ]; then
    echo 'publish failed'
else 
    explorer "bin/$build/$target/"
fi
