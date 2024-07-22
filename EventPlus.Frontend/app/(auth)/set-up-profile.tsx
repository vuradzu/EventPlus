import React from "react";
import { Image, SafeAreaView, TouchableOpacity, View } from "react-native";

import { Button, ButtonVariants } from "~/components/core/Button/Button";
import Input from "~/components/core/Input/Input";
import { Typography } from "~/components/core/Typography/Typography";

import { InputVariant } from "~/components/core/Input/types/InputVariant";
import { useSetUpProfileCubit } from "./services/useSetUpProfileCubit";

const SetUpProfile = () => {
  const {
    isLoading,
    pickImage,
    onSubmit,
    form,
    setFormValue,
    formErrors,
    accountImage,
  } = useSetUpProfileCubit();

  return (
    <SafeAreaView className="bg-bg-primary-vr w-full h-full">
      <View className="flex flex-col justify-between w-full h-full pt-10 px-4">
        {/* profile info section */}
        <View className="items-center">
          <TouchableOpacity
            onPress={pickImage}
            className="w-[100px] 
              h-[100px]
              items-center
              justify-center
              bg-bg-inverse
              rounded-full
              overflow-hidden
              mb-5"
          >
            <Image
              source={accountImage}
              resizeMode="cover"
              className={"w-full h-full"}
            />
          </TouchableOpacity>
          <Typography
            className="color-text-system text-[17px]"
            onPress={pickImage}
          >
            Змінити світлину
          </Typography>

          <View className="items-center mt-5 w-full">
            <Input
              styles="mb-4"
              placeholder="Юзернейм"
              variant={InputVariant.FullyRounded}
              value={form.username}
              onValueChange={(value) => setFormValue("username", value ?? "")}
              error={formErrors.username}
            />
            <Input
              disabled
              styles="mb-4"
              placeholder="Пошта"
              variant={InputVariant.FullyRounded}
              value={form.email}
              onValueChange={(value) => setFormValue("email", value ?? "")}
              error={formErrors.email}
            />
            <Input
              styles="mb-4"
              placeholder="Ім'я"
              variant={InputVariant.FullyRounded}
              value={form.firstName}
              onValueChange={(value) => setFormValue("firstName", value ?? "")}
              error={formErrors.firstName}
            />
            <Input
              placeholder="Прізвище"
              variant={InputVariant.FullyRounded}
              value={form.lastName}
              onValueChange={(value) => setFormValue("lastName", value ?? "")}
              error={formErrors.lastName}
            />
          </View>
        </View>

        {/* button */}
        <View>
          <Button
            isLoading={isLoading}
            variant={ButtonVariants.PrimaryBold}
            onPress={onSubmit}
          >
            Продовжити
          </Button>
        </View>
      </View>
    </SafeAreaView> 
  );
};

export default SetUpProfile;
