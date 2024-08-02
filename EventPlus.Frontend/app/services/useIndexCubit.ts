import { router } from "expo-router";
import { useCommandsStore } from "~/store/commands/commands.store";
import { useUserStore } from "~/store/user/user.store";
import { JwtHelper } from "~/utils/helpers/jwtHelper";

export const useIndexCubit = () => {
  const { clearStore } = useUserStore();
  const { activeCommand, clearStore: clearCommands } = useCommandsStore();

  const onAppEnter = async () => {
    // clearStore();
    // clearCommands();
    const tokenInfo = JwtHelper.getTokenInfo();

    if (!tokenInfo) {
      router.replace("sign-in");
      return;
    }

    const tokenValid = !JwtHelper.isTokenExpired(tokenInfo.tokenExpires);

    if (!tokenValid) await JwtHelper.refreshToken();

    if (!!activeCommand) {
      router.replace("home/home");
      return;
    }

    router.push("command-onboarding");
  };

  return { onAppEnter };
};
