import { router } from "expo-router";
import { useUserStore } from "~/store/user/user.store";
import { JwtHelper } from "~/utils/helpers/jwtHelper";

export const useIndexCubit = () => {
  const { activeCommand, storeUser, clearStore } = useUserStore();

  const onAppEnter = async () => {
    console.warn(storeUser)
    console.warn(activeCommand)
    const tokenInfo = JwtHelper.getTokenInfo();

    if (!tokenInfo) {
      console.warn('no token', tokenInfo)
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
