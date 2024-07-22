import React from "react";
import { View } from "react-native";
import { Typography } from "~/components/core/Typography/Typography";
import { EventModel } from "./EventTilesDashboard";

interface EventTileProps {
  event: EventModel;
}

export const EventTile = ({event}: EventTileProps) => {
  const { title } = event;

  return (
    <View className="flex items-center w-[114px] h-[114px]">
      <View className="w-[114px] h-[114px] bg-bg-inverse rounded-2xl"></View>
      <Typography className="color-text-primary mt-1">{title}</Typography>
    </View>
  );
};
