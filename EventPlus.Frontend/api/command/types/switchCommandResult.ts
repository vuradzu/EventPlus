import { CommandModel } from "./commandModel";
import { CommandTokensModel } from "./commandTokensModel";

export interface SwitchCommandResult {
  command: CommandModel;
  tokens: CommandTokensModel;
}
