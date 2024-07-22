import { router } from "expo-router";
import LottieView from "lottie-react-native";
import { View } from "react-native";
import { useUserStore } from "~/store/user/user.store";

const App = () => {
  const { storeUser, clearStore } = useUserStore();

  const animation = !!storeUser
    ? require("~/assets/json/splash-full.json")
    : require("~/assets/json/splash-half.json");

  return (
    <View className="w-full h-full bg-bg-primary">
      <LottieView
        style={{ flex: 1 }}
        source={animation}
        autoPlay
        loop={false}
        speed={1.2}
        onAnimationFinish={() => {
          // clearStore();

          if (storeUser === null) {
            router.replace("sign-in");
          }

          if (!!storeUser) {
            router.replace("home/home");
          }
        }}
      />
    </View>
  );
};

export default App;
