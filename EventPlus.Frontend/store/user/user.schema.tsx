export type StoreTokenInfo = {
  commandId?: number | null;

  token: string;
  tokenExpires: Date;

  refreshToken: string;
  refreshTokenExpires: Date;
};

export type StoreUser = {
  username: string;
  firstName: string;
  lastName: string | null;
  avatar: string | null;
  tokens: StoreTokenInfo[];
  commands: number[];
};

interface StoreData {
  storeUser: StoreUser | null;
}

interface StoreActions {
  setStoreUser: (storeUser: Omit<StoreUser, "tokens">) => void;
  addUserTokenInfo: (tokenInfo: StoreTokenInfo) => void;
  updateUserAvatar: (url: string) => void;
  clearStore: () => void;
}

export type UserStoreSchema = StoreData & StoreActions;
