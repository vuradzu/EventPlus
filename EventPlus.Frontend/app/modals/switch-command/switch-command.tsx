import { useQuery } from "@tanstack/react-query";
import React, { useMemo } from "react";
import { View } from "react-native";
import { _getAllCommands } from "~/api/command/command.api";
import { Typography } from "~/components/core/Typography/Typography";
import { QueryKeys } from "~/utils/helpers/queryKeys";
import { baseModalScreenOptions } from "../modalsBaseOptions";
import {
  CommandSelection,
  CommandSelectionSkeleton,
} from "./components/CommandSelection";
import { CreateCommandSelection } from "./components/CreateCommandSelection";
import { useSwitchCommandCubit } from "./services/useSwitchCommandCubit";

const SwitchCommand = () => {
  const { calculateModalWidth, switchCommand } = useSwitchCommandCubit();

  const commandsAccessor = useQuery({
    queryKey: QueryKeys.UserCommands,
    queryFn: _getAllCommands,
  });

  const modalHeight = useMemo(() => {
    return calculateModalWidth(commandsAccessor.data?.length);
  }, [commandsAccessor]);

  return (
    <View
      style={{ flex: 1, flexDirection: "column", justifyContent: "flex-end" }}
    >
      <View
        style={{ minHeight: modalHeight }}
        className="w-full bg-bg-surface-1-strong rounded-t-[8px] px-4"
      >
        {/* Header */}
        <View className="flex justify-center items-center my-4">
          <Typography variant="b1" fontWeight="bold">
            Мої команди {commandsAccessor.isFetching.toString()}
          </Typography>
        </View>

        {/* Body */}
        <View className="flex flex-1 flex-col justify-between">
          <View className="flex flex-col">
            {commandsAccessor.isFetching
              ? Array.from(Array(2).keys()).map((i) => (
                  <CommandSelectionSkeleton key={i} />
                ))
              : commandsAccessor.isSuccess
              ? commandsAccessor.data.map((c) => (
                  <CommandSelection key={c.id} onClick={() => switchCommand(c.id)} command={c} />
                ))
              : null}
          </View>

          <View className="mb-8">
            <CreateCommandSelection />
          </View>
        </View>
      </View>
    </View>
  );
};

export default SwitchCommand;

export const switchCommandModalOptions: any = {
  ...baseModalScreenOptions,
  headerShown: false,
  headerTitle: "Мої команди",
  headerStyle: {
    backgroundColor: "#272727",
  },
  headerTitleStyle: {
    color: "#FFFFFF",
  },
  headerLeft: undefined,
  contentStyle: {
    backgroundColor: "transparent",
  },
};
