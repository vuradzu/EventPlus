import LottieView from "lottie-react-native";
import { View } from "react-native";
import { JwtHelper } from "~/utils/helpers/jwtHelper";
import { useIndexCubit } from "./services/useIndexCubit";

const App = () => {
  const { onAppEnter } = useIndexCubit();
  const needSso = !JwtHelper.getTokenInfo();

  const animation = needSso
    ? require("~/assets/json/splash-half.json")
    : require("~/assets/json/splash-full.json");

  return (
    <View className="w-full h-full bg-bg-primary">
      <LottieView
        style={{ flex: 1 }}
        source={animation}
        autoPlay
        loop={false}
        speed={1.2}
        onAnimationFinish={onAppEnter}
      />
    </View>
  );
};

export default App;
