import { CommandModel } from "~/api/command/types/commandModel";

export interface JwtResult {
  username: string;
  firstName: string;
  lastName: string | null;

  token: string;
  tokenExpires: Date;

  refreshToken: string;
  refreshTokenExpires: Date;

  avatar: string | null;
  commands: number[];
  lastActivityCommand: CommandModel | null;
}
