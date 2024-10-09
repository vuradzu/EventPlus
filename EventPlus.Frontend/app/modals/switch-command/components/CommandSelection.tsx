import { Skeleton } from "moti/skeleton";
import React from "react";
import { View } from "react-native";
import { TouchableOpacity } from "react-native-gesture-handler";
import { CommandModel } from "~/api/command/types/commandModel";
import { Iconify } from "~/components/core/Iconify/Iconify";
import { Typography } from "~/components/core/Typography/Typography";
import { useCommandsStore } from "~/store/commands/commands.store";

interface CommandSelectionProps {
  command: Partial<CommandModel>;
  onClick?: () => void;
}

export const CommandSelection = ({ command, onClick }: CommandSelectionProps) => {
  const { activeCommand } = useCommandsStore();

  return (
    <TouchableOpacity onPress={onClick} className="flex flex-row my-2 justify-between items-center">
      <View className="flex flex-row items-center">
        {/* Image */}
        <View className="w-[48px] h-[48px] rounded-[8px] flex justify-center items-center bg-text-system">
          <Iconify icon="emojione-monotone:star-of-david" color="white"/>
        </View>
        <View className="flex flex-column ml-3">
          <Typography fontWeight="bold">{command.name}</Typography>
        </View>
      </View>
      <View>
        {command.id === activeCommand ? (
          <Iconify icon="pajamas:check-circle-filled" color="#007AFF" />
        ) : null}
      </View>
    </TouchableOpacity>
  );
};

export const CommandSelectionSkeleton = () => {
  return (
    <View className="flex flex-row my-2 items-center">
      {/* Image skeleton*/}
      <Skeleton width={48} height={48} />
      <View className="ml-3" >
        <Skeleton height={17} width={250} />
      </View>
    </View>
  );
};
