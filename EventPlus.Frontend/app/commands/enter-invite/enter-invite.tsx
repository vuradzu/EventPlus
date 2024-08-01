import LottieView from "lottie-react-native";
import React from "react";
import { SafeAreaView, View } from "react-native";

import UserPlus from "~/assets/icons/user-plus.png";
import CodeInput from "~/components/CodeInput/CodeInput";
import { Button, ButtonVariants } from "~/components/core/Button/Button";
import { TypographyVariants } from "~/components/core/Typography/types/TypographyVariants";
import { Typography } from "~/components/core/Typography/Typography";
import { useEnterInviteCubit } from "./services/useEnterInviteCubit";

const EnterInvite = () => {
  const { onSubmit, codeForm, setCodeForm, codeErrors, isLoading } =
    useEnterInviteCubit();

  return (
    <SafeAreaView className="bg-bg-primary-vr w-full h-full">
      <View className="flex flex-col w-full h-full items-center justify-between px-4'">
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
          <Typography
            fontSize={38}
            variant={TypographyVariants.Semibold}
            className="mb-3 text-center"
          >
            Введіть код запрошення
          </Typography>
          <Typography
            fontSize={23}
            className="color-text-secondary text-center"
          >
            Для отримання коду зверніться до адміністратора команди
          </Typography>
          <CodeInput
            styles="mt-8 px-16"
            cellsCount={5}
            value={codeForm.code ?? ""}
            setValue={(code) => setCodeForm({ code })}
            error={codeErrors.code}
          />
        </View>

        {/* buttons */}
        <View className="w-full">
          <Button
            styles="mb-4"
            variant={ButtonVariants.PrimaryBold}
            icon={UserPlus}
            iconPosition="right"
            onPress={onSubmit}
            isLoading={isLoading}
          >
            Додатись до команди
          </Button>
        </View>
      </View>
    </SafeAreaView>
  );
};

export default EnterInvite;
