import { router } from "expo-router";
import { Skeleton } from "moti/skeleton";
import React, { useMemo } from "react";
import { View } from "react-native";
import { useCommandsStore } from "~/store/commands/commands.store";
import { IconButton } from "../core/Button/IconButton";
import { Typography } from "../core/Typography/Typography";

export const CommandHeader = () => {
  const { activeCommand, commandLoading, commands } = useCommandsStore();

  const currentCommand = useMemo(
    () => commands.find((c) => c.id === activeCommand),
    [activeCommand]
  );

  return (
    <View className="h-[50px] flex flex-row justify-between items-center border-b border-border-primary px-4 py-2">
      <View className="flex flex-row">
        <View>
          <IconButton
            styles="w-[32px] h-[32px]"
            iconProps={{ icon: { uri: currentCommand?.avatar } }}
          />
        </View>
        <View>
          <View>
            <Typography variant="l" fontWeight="bold">
              {commandLoading ? (
                <Skeleton height={11} width={100} />
              ) : (
                currentCommand?.name ?? "Немає активної команди"
              )}
            </Typography>
          </View>
          <View className="mt-[2px]">
            <Typography variant="l">
              {commandLoading ? (
                <Skeleton height={11} width={150} />
              ) : (
                currentCommand?.avatar ?? "Нема"
              )}
            </Typography>
          </View>
        </View>
      </View>
      <View className="p-[6px] bg-button-bg-secondary rounded-lg">
        <IconButton
          onPress={() => router.push("modals/switch-command/switch-command")}
          iconProps={{
            icon: "hugeicons:user-multiple-02",
            width: 20,
            height: 20,
            color: "white",
          }}
        />
      </View>
    </View>
  );
};
