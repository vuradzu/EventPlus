import dayjs from "dayjs";
import React from "react";
import { TouchableOpacity, View } from "react-native";
import DateTimePicker, { DateType } from "react-native-ui-datepicker";
import { DatePickerSingleProps } from "react-native-ui-datepicker/lib/typescript/src/DateTimePicker";
import { classNames } from "~/utils/helpers/classNames";
import { Typography } from "../Typography/Typography";

interface CalendarProps {
  date: DateType;
  setDate: (date: DateType) => void;
  useTime?: boolean;
}

export const Calendar = (props: CalendarProps) => {
  const { date, setDate, useTime = true } = props;

  const styles: Partial<DatePickerSingleProps> = {
    todayContainerStyle: {
      borderColor: "#FFFFFF24",
      borderRadius: 25,
    },
    todayTextStyle: { color: "white" },
    headerTextStyle: {
      color: "white",
    },
    headerContainerStyle: {
      paddingTop: 10,
    },
    headerButtonColor: "white",
    selectedTextStyle: { color: "white" },
    timePickerTextStyle: { color: "white" },
    timePickerIndicatorStyle: { backgroundColor: "#7560F1" },
    calendarTextStyle: { color: "white" },
    weekDaysTextStyle: { color: "white" },
    selectedItemColor: "#7560F1",
    yearContainerStyle: {
      backgroundColor: "transparent",
      borderColor: "#FFFFFF24",
    },
    monthContainerStyle: {
      backgroundColor: "transparent",
      borderColor: "#FFFFFF24",
    },
  };

  return (
    <View
      className="bg-bg-primary rounded-xl"
      style={{ width: 340, paddingHorizontal: 15 }}
    >
      <DateTimePicker
        {...styles}
        mode="single"
        timePicker={true}
        date={date}
        onChange={(params) => setDate(params.date)}
        locale={"uk"}
      />
      {/* footer */}
      <View className="flex flex-row justify-between items-center mt-[-5] mb-4">
        <Typography>
          {date
            ? dayjs(date)
                .locale("uk")
                .format(useTime ? "MMMM, DD, YYYY - HH:mm" : "MMMM, DD, YYYY")
            : "..."}
        </Typography>
        <TouchableOpacity
          className={classNames("w-[30%]", {}, [
            "bg-button-bg-primary",
            "text-center",
            "rounded-lg",
            "py-2",
          ])}
          onPress={(event) => {
            event.preventDefault();
            setDate(dayjs());
          }}
          accessibilityRole="button"
          accessibilityLabel="Today"
        >
          <Typography fontWeight="semibold" className="text-center">
            Сьогодні
          </Typography>
        </TouchableOpacity>
      </View>
    </View>
  );
};
