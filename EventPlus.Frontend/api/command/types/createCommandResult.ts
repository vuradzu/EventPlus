import { CommandModel } from "./commandModel";
import { CommandTokensModel } from "./commandTokensModel";

export interface CreateCommandResult extends CommandModel {
  tokens: CommandTokensModel;
}
