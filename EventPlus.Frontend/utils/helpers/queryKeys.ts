export class QueryKeys {
  public static Keys = {
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
}
