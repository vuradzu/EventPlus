import React from "react";
import { SafeAreaView, View } from "react-native";
import { Button, ButtonVariants } from "~/components/core/Button/Button";
import { CalendarInput } from "~/components/core/CalendarInput/CalendarInput";
import Input from "~/components/core/Input/Input";
import { InputVariant } from "~/components/core/Input/types/InputVariant";
import PriorityTabs from "~/components/core/PriorityTabs/PriorityTabs";
import { Typography } from "~/components/core/Typography/Typography";
import { baseModalScreenOptions } from "../modalsBaseOptions";
import { useCreateEventCubit } from "./services/useCreateEventCubit";

const CreateEvent = () => {
  const { form, setFormValue, isLoading, formErrors, onSubmit } =
    useCreateEventCubit();

  return (
    <SafeAreaView className="w-full h-full bg-bg-surface-1-strong">
      <View className="w-full h-full flex-col mt-5 px-4 justify-between">
        {/* form */}
        <View>
          <Typography fontWeight="semibold" className="color-text-primary mb-2">
            Назва
          </Typography>
          <Input
            value={form.title}
            onValueChange={(title) => setFormValue("title", title ?? "")}
            placeholder="Введіть назву події"
            variant={InputVariant.HalfRounded}
            styles="mb-3"
            error={formErrors.title}
          />

          <Typography fontWeight="semibold" className="color-text-primary mb-2">
            Опис
          </Typography>
          <Input
            value={form.description}
            onValueChange={(description) => {
              if (description === "") {
                setFormValue("description", undefined);
                return;
              }

              setFormValue("description", description);
            }}
            multiline
            numberOnLines={2}
            maxLength={100}
            placeholder="Введіть опис події"
            variant={InputVariant.HalfRounded}
            styles="mb-3"
            error={formErrors.description}
          />

          <Typography fontWeight="semibold" className="color-text-primary mb-2">
            Пріоритет
          </Typography>
          <PriorityTabs
            priority={form.priority}
            onPriorityChange={(priority) => setFormValue("priority", priority)}
            styles="mb-3"
          />

          <Typography fontWeight="semibold" className="mb-2">
            Дата події
          </Typography>
          <CalendarInput
            date={form.date}
            setDate={(date) => setFormValue("date", date)}
            error={formErrors.date}
          />
        </View>

        {/* button */}
        <View className="mb-4">
          <Button
            onPress={onSubmit}
            isLoading={isLoading}
            variant={ButtonVariants.PrimaryBold}
          >
            Створити подію
          </Button>
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
