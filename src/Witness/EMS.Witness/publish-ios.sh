 while getopts "p:" option; do
  case $option in
    p) mac_pass=$OPTARG;;
    \?) echo "Error: Invalid option"
      exit;;
   esac
done

# Require -p argument
if [ -z "$mac_pass" ]; then
  echo "Error: password for remote macOS user is required in order build iOS APP"
  exit 1
fi

# Current remote macOS setup is MacinCloud. The following settings have to be updated if anything changes on their end
# mac_ip
# mac_user
# mac_pass - provide using -p argument in CLI
target=net8.0-ios
build=Release
mac_ip=195.82.55.68
mac_user=user936808
tcp_port=58181
mac_dotnet_root="/Users/$mac_user/Library/Caches/Xamarin/XMA/SDKs/dotnet/"

echo $tcp_port

dotnet publish \
 -f "$target" \
 -c "$build" \
 -p:ServerAddress="$mac_ip" \
 -p:ServerUser="$mac_user" \
 -p:ServerPassword="$mac_pass" \
 -p:TcpPort="$tcp_port" \
 -p:DotNetRootRemoteDirectory="$mac_dotnet_root"

if [ $? -eq 1 ]; then
    echo 'publish failed'
else
    cd "bin/$build/$target"
    explorer .
    cd -
fi
