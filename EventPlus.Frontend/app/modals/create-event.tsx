import React, { useState } from "react";
import { SafeAreaView, View } from "react-native";
import { Button } from "~/components/core/Button/Button";
import Input from "~/components/core/Input/Input";
import PriorityTabs from "~/components/core/PriorityTabs/PriorityTabs";
import { TypographyVariants } from "~/components/core/Typography/types/TypographyVariants";
import { Typography } from "~/components/core/Typography/Typography";
import { baseModalScreenOptions } from "./modalsBaseOptions";
import { InputVariant } from "~/components/core/Input/types/InputVariant";

const CreateEvent = () => {
  const [state, setState] = useState<string>("");

  return (
    <SafeAreaView
      className="w-full h-full bg-bg-surface-1-strong"
    >
      <View className="w-full h-full flex-col mt-5 px-4 justify-between">
        {/* form */}
        <View>
          <Typography
            className="color-text-primary mb-2"
            variant={TypographyVariants.Semibold}
          >
            Назва
          </Typography>
          <Input
            value={""}
            onValueChange={() => {}}
            placeholder="Введіть назву події"
            variant={InputVariant.HalfRounded}
            styles="mb-3"
          />

          <Typography
            className="color-text-primary mb-2"
            variant={TypographyVariants.Semibold}
          >
            Опис
          </Typography>
          <Input
            value={""}
            onValueChange={() => {}}
            multiline
            numberOnLines={2}
            maxLength={100}
            placeholder="Введіть опис події"
            variant={InputVariant.HalfRounded}
            styles="mb-3"
          />

          <Typography
            className="color-text-primary mb-2"
            variant={TypographyVariants.Semibold}
          >
            Пріоритет
          </Typography>
          <PriorityTabs priority="medium" />
        </View>

        {/* button */}
        <View className="mb-4">
          <Button>Створити подію</Button>
        </View>
      </View>
    </SafeAreaView>
  );
};

export default CreateEvent;

export const createEventModalOptions: any = {
  ...baseModalScreenOptions,
  headerTitle: "Нова подія",
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
