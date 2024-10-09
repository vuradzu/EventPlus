import { useLocalSearchParams } from "expo-router";
import React from "react";
import { SafeAreaView, View } from "react-native";
import { Button, ButtonVariants } from "~/components/core/Button/Button";
import Input from "~/components/core/Input/Input";
import { InputVariant } from "~/components/core/Input/types/InputVariant";
import { Typography } from "~/components/core/Typography/Typography";
import { baseModalScreenOptions } from "../modalsBaseOptions";
import { useCreateCommandCubit } from "./services/useCreateCommandCubit";

const CreateCommand = () => {
  const { closeAllModals } = useLocalSearchParams();
  const { form, setForm, formErrors, onSubmit, isLoading } =
    useCreateCommandCubit(closeAllModals === "true");

  return (
    <SafeAreaView className="w-full h-full bg-bg-surface-1-strong">
      <View className="w-full h-full flex-col mt-5 px-4 justify-between">
        {/* form */}
        <View>
          <Typography fontWeight="semibold" className="color-text-primary mb-2">
            Назва
          </Typography>
          <Input
            value={form.name}
            onValueChange={(name) => setForm({ ...form, name })}
            placeholder="Введіть назву команди"
            variant={InputVariant.HalfRounded}
            styles="mb-3"
            error={formErrors.name}
          />

          <Typography fontWeight="semibold" className="color-text-primary mb-2">
            Опис
          </Typography>
          <Input
            value={form.description}
            onValueChange={(description) => setForm({ ...form, description })}
            multiline
            numberOnLines={2}
            maxLength={100}
            placeholder="Введіть опис команди"
            variant={InputVariant.HalfRounded}
            styles="mb-3"
          />
        </View>

        {/* button */}
        <View className="mb-4">
          <Button
            onPress={onSubmit}
            isLoading={isLoading}
            variant={ButtonVariants.PrimaryBold}
          >
            Створити команду
          </Button>
        </View>
      </View>
    </SafeAreaView>
  );
};

export default CreateCommand;

export const createCommandModalOptions: any = {
  ...baseModalScreenOptions,
  headerTitle: "Нова команда",
  headerStyle: {
    backgroundColor: "#272727",
  },
  headerTitleStyle: {
    color: "#FFFFFF",
  },
  contentStyle: {
    borderTopWidth: 2,
    borderTopColor: "#3E3E3E",
  },
};
