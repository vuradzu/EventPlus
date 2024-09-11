import { router } from "expo-router";
import LottieView from "lottie-react-native";
import React from "react";
import { SafeAreaView, View } from "react-native";
import UserPlus from "~/assets/icons/user-plus.png";
import UsersPlus from "~/assets/icons/users-plus.png";
import { Button, ButtonVariants } from "~/components/core/Button/Button";
import { Typography } from "~/components/core/Typography/Typography";

const CommandOnboarding = () => {
  return (
    <SafeAreaView className="bg-bg-primary-vr w-full h-full">
      <View className="flex flex-col w-full h-full items-center justify-between px-4">
        {/* icon and text */}
        <View className="flex flex-col h-[30%]">
          <View className="mb-1 h-[70%]">
            <LottieView
              style={{ flex: 1 }}
              source={require("~/assets/json/command-onboarding.json")}
              autoPlay
              loop={false}
              speed={1}
            />
          </View>
          <Typography variant="h2" fontWeight="semibold" className="mb-3 text-center">
            Продовжити
          </Typography>
          <Typography className="color-text-secondary text-center">
            Як ви бажаєте продовжити?
          </Typography>
        </View>

        {/* buttons */}
        <View className="w-full">
          <Button
            styles="mb-4"
            variant={ButtonVariants.PrimaryBold}
            iconProps={{icon: "mdi:user", color: "white"}}
            iconPosition="right"
            onPress={() => router.push("modals/create-command/create-command")}
          >
            Створити нову команду
          </Button>
          <Button
            variant={ButtonVariants.SecondaryBold}
            iconProps={{
              icon: "mdi:users-plus",
              color: "white"
            }}
            iconPosition="right"
            onPress={() => router.push("commands/enter-invite/enter-invite")}
          >
            Додатись до існуючої
          </Button>
        </View>
      </View>
    </SafeAreaView>
  );
};

export default CommandOnboarding;
