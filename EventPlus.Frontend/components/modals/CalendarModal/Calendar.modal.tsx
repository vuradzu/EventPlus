import React, { useState } from "react";
import { View } from "react-native";
import { DateType } from "react-native-ui-datepicker";
import { Calendar } from "~/components/core/Calendar/Calendar";

interface CalendarModalProps {
  selectedDate?: DateType;
  onDateChange: (date: DateType) => void;
}

export const CalendarModal = ({
  selectedDate,
  onDateChange,
}: CalendarModalProps) => {
  const [date, setDate] = useState<DateType>(selectedDate);

  const onDateChangeHandler = (date: DateType) => {
    setDate(date);
    onDateChange(date);
  };

  return (
    <View className="flex items-center">
      <Calendar date={date} setDate={onDateChangeHandler} />
    </View>
  );
};
