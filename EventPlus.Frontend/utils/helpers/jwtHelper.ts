import { router } from "expo-router";
import { authenticate, refresh } from "~/api/jwt/jwt.api";
import { AuthenticateRequest } from "~/api/jwt/types/authenticateRequest";
import { JwtResult } from "~/api/jwt/types/jwtResult";
import { StoreTokenInfo } from "~/store/user/user.schema";
import { userStore } from "~/store/user/user.store";

export class JwtHelper {
  public static getTokenInfo(): StoreTokenInfo | undefined {
    const commandId = userStore.getState().activeCommand;

    if (!commandId) return this.getAccessTokenWithoutCommandId();

    return this.getAccessTokenWithCommandId(commandId);
  }

  public static isAccessTokenValid() {
    const tokenInfo = this.getTokenInfo();

    return !!tokenInfo && !this.isTokenExpired(tokenInfo.tokenExpires);
  }

  public static isTokenExpired(expirationDate: Date) {
    return (
      !expirationDate ||
      new Date(expirationDate).getTime() < new Date().getTime()
    );
  }
  public static async authenticateUser(
    request: Partial<AuthenticateRequest>
  ): Promise<JwtResult> {
    const { setStoreUser } = userStore.getState();

    const jwtResult = await authenticate(request);

    setStoreUser({
      firstName: jwtResult.firstName,
      lastName: jwtResult.lastName,
      username: jwtResult.username,
      avatar: jwtResult.avatar,
      commands: jwtResult.commands,
    });

    this.addUserToken(jwtResult, jwtResult.lastActivityCommand);

    return jwtResult;
  }

  public static async refreshToken() {
    const tokenInfo = this.getTokenInfo();
    if (!tokenInfo || JwtHelper.isTokenExpired(tokenInfo.refreshTokenExpires)) {
      router.replace("sign-in");
      return;
    }

    const { activeCommand, setStoreUser } = userStore.getState();

    const refreshJwtResult = await refresh(
      tokenInfo.refreshToken,
      activeCommand
    );

    setStoreUser({
      firstName: refreshJwtResult.firstName,
      lastName: refreshJwtResult.lastName,
      username: refreshJwtResult.username,
      avatar: refreshJwtResult.avatar,
      commands: refreshJwtResult.commands,
    });

    this.addUserToken(refreshJwtResult);
  }

  public static addUserToken(
    jwtResult: Pick<
      JwtResult,
      "token" | "tokenExpires" | "refreshToken" | "refreshTokenExpires"
    >,
    commandId?: number | null
  ) {
    const { addUserTokenInfo } = userStore.getState();

    addUserTokenInfo({
      commandId: commandId,
      token: jwtResult.token,
      tokenExpires: jwtResult.tokenExpires,
      refreshToken: jwtResult.refreshToken,
      refreshTokenExpires: jwtResult.refreshTokenExpires,
    });
  }

  private static getAccessTokenWithoutCommandId() {
    const tokenWithoutCommandAccess = userStore
      .getState()
      .storeUser?.tokens?.find(
        (ti) =>
          !("commandId" in ti) ||
          ti.commandId === null ||
          ti.commandId === undefined
      );

    return tokenWithoutCommandAccess;
  }

  private static getAccessTokenWithCommandId(commandId: number) {
    const commandAccessToken = userStore
      .getState()
      .storeUser?.tokens?.find((ti) => ti.commandId === commandId);

    return commandAccessToken;
  }
}
