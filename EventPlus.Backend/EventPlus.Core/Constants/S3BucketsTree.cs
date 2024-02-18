namespace EventPlus.Core.Constants;

public static class S3BucketsTree
{
    public static CommandsTree CommandsBucket => new();
    public static UsersTree UsersBucket => new();
}

public class UsersTree : S3Folder
{
    public string Avatars => nameof(Avatars);
    public string CommandMembersAvatars => nameof(CommandMembersAvatars);
}

public class CommandsTree: S3Folder
{
    public string Avatars => nameof(Avatars);
}

public abstract class S3Folder
{
    public string Root => string.Empty;
}