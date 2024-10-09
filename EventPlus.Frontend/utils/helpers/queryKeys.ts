export class QueryKeys {
  public static Keys = {
    user: "user",
    events: "events",
    event: "event",
    command: "command",
    commands: "commands",
  };

  public static EventsByCommand = (commandId: number) => [
    this.Keys.events,
    this.Keys.command,
    commandId,
  ];

  public static UserCommands = [this.Keys.user, this.Keys.commands];
}
