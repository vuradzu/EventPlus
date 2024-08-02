import dayjs from "dayjs";
import React, { useCallback } from "react";
import { View } from "react-native";
import { TouchableOpacity } from "react-native-gesture-handler";
import { magicModal } from "react-native-magic-modal";
import { DateType } from "react-native-ui-datepicker";
import CalendarIcon from "~/assets/icons/calendar.png";
import { CalendarModal } from "~/components/modals/CalendarModal/Calendar.modal";
import { ClassNameProps, classNames } from "~/utils/helpers/classNames";
import { InputIcon } from "../Input/components/InputIcon";
import { Typography } from "../Typography/Typography";
import { InputError } from "../Input/components/InputError";

interface CalendarInputProps {
  date?: DateType;
  setDate: (date: DateType) => void;
  useTime?: boolean;
  error?: string;
}

export const CalendarInput = (
  props: ClassNameProps<CalendarInputProps, false>
) => {
  const { styles, date, setDate, useTime = true, error } = props;

  const showCalendarModal = useCallback(() => {
    magicModal.show(() => <CalendarModal onDateChange={setDate} />, {
      swipeDirection: undefined,
    });
  }, [date]);

  return (
    <View className={classNames(styles, {}, ["flex", "flex-col"])}>
      <TouchableOpacity
        className={classNames("flex", {}, [
          "h-[58px]",
          "bg-input-bg",
          "rounded-xl",
          "flex-row",
          "items-center",
          "pl-4",
        ])}
        onPress={showCalendarModal}
      >
        <InputIcon icon={CalendarIcon} />
        <Typography fontSize={14} className="ml-2">
          {date
            ? dayjs(date)
                .locale("uk")
                .format(useTime ? "MMMM, DD, YYYY - HH:mm" : "MMMM, DD, YYYY")
            : "..."}
        </Typography>
      </TouchableOpacity>
      <InputError error={error} />
    </View>
  );
};
