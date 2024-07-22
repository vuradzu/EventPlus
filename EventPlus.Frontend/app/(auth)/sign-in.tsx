import { Animated, Image, View } from "react-native";

import { Button, ButtonVariants } from "~/components/core/Button/Button";

import LottieView from "lottie-react-native";
import { SafeAreaView } from "react-native-safe-area-context";
import { TypographyVariants } from "~/components/core/Typography/types/TypographyVariants";
import { Typography } from "~/components/core/Typography/Typography";
import { useSignInCubit } from "./services/useSignInCubit";

import { useEffect, useRef } from "react";
import GoogleWhite from "~/assets/icons/socials/google_white.png";
import { HelloWave } from "~/components/HelloWave/HelloWave";

const SignIn = () => {
  const { signOn, signOnIsLoading } = useSignInCubit();

  const opacity = useRef(new Animated.Value(0)).current;

  useEffect(() => {
    Animated.timing(opacity, {
      toValue: 1,
      duration: 1000,
      useNativeDriver: true,
    }).start();
  }, []);

  return (
    <View className="w-full h-full bg-bg-primary">
      <LottieView
        style={{ flex: 1 }}
        source={require("~/assets/json/sign-in.json")}
        autoPlay
        loop={false}
        speed={1}
      />
      <SafeAreaView
        style={{
          position: "absolute",
          top: 20,
          left: 0,
        }}
        className="w-full h-full m-0 p-0"
      >
        <Animated.View style={{ opacity }}>
          <View className="w-full h-full px-4 flex-col justify-between">
            <View className="w-full px-[16px] py-[40px]">
              <HelloWave />
              <Typography
                fontSize={36}
                className="mb-2"
                variant={TypographyVariants.Semibold}
              >
                Привіт!
              </Typography>
              <Typography fontSize={16} className="mb-[50px]">
                Увійти, щоб продовжити
              </Typography>

              <View className="w-full">
                <Button
                  icon={GoogleWhite}
                  variant={ButtonVariants.PrimaryBold}
                  styles="my-[8px]"
                  isLoading={signOnIsLoading}
                  onPress={signOn}
                >
                  Увійти за допомогою Google
                </Button>
              </View>
            </View>
            <View className="w-full flex justify-center items-center opacity-30">
              <Image
                source={require("~/assets/images/logo_big.png")}
                className="w-[65px]"
                resizeMode="contain"
              />
            </View>
          </View>
        </Animated.View>
      </SafeAreaView>
    </View>
  );
};

export default SignIn;
