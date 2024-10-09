import { router } from "expo-router";
import { Dimensions } from "react-native";
import { _switchCommand } from "~/api/command/command.api";
import { useCommandsStore } from "~/store/commands/commands.store";
import { JwtHelper } from "~/utils/helpers/jwtHelper";

export const useSwitchCommandCubit = () => {
  const { addCommand, setActiveCommand, setCommandLoading } =
    useCommandsStore();

  const calculateModalWidth = (count?: number) => {
    const startHeight = Dimensions.get("window").height / 2;

    if (!count) return startHeight;

    if (count === 5) return startHeight + 15;

    if (count > 5) {
      const fiveTilesHeight = startHeight + 15;
      const countToMultiply = count - 5;
      const tileHeight = 64;

      return fiveTilesHeight + tileHeight * countToMultiply;
    }

    if (count > 10) return "100%";

    return startHeight;
  };

  const switchCommand = async (commandId: number) => {
    router.back();

    setCommandLoading(true);

    const result = await _switchCommand(commandId);

    addCommand(result.command);
    setActiveCommand(result.command.id);
    JwtHelper.addUserToken(result.tokens, result.command.id);

    setCommandLoading(false);
  };

  return { calculateModalWidth, switchCommand };
};
