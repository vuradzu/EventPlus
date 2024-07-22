export interface AuthenticateRequest {
  username: string;
  email?: string | null;
  firstName?: string | null;
  lastName?: string | null;
  avatar?: string | null;
  commandId?: number | null;

  providerMetadata: Record<string, string | null>;
  provider: "google";
  providerKey: string;

  type: "register" | "login";
}
