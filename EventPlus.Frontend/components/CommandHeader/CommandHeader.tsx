import React, { useMemo } from "react";
import { View } from "react-native";
import { useCommandsStore } from "~/store/commands/commands.store";
import { IconButton } from "../core/Button/IconButton";
import { Typography } from "../core/Typography/Typography";

export const CommandHeader = () => {
  const { activeCommand, commands } = useCommandsStore();

  const currentCommand = useMemo(
    () => commands.find((c) => c.id === activeCommand),
    [activeCommand]
  );

  return (
    <View className="h-[50px] flex justify-center border-b border-border-primary px-4 py-2">
      <View className="flex flex-row">
        <View>
          <IconButton
            styles="w-[32px] h-[32px]"
            iconProps={{ icon: { uri: currentCommand?.avatar } }}
          ></IconButton>
        </View>
        <View>
          <Typography variant="l" fontWeight="bold">
            {currentCommand?.name ?? "Немає активної команди"}
          </Typography>
          <Typography variant="l">
            {currentCommand?.avatar ?? "Нема"}
          </Typography>
        </View>
      </View>
      <View></View>
    </View>
  );
};
