import { useStore } from "zustand";
import { createStore } from "~/utils/helpers/storeHelpers";
import { StoreTokenInfo, StoreUser, UserStoreSchema } from "./user.schema";

export const userStore = createStore<UserStoreSchema>(
  "User Store",
  (set) => {
    const setStoreUser = (user: Omit<StoreUser, "tokens">) => {
      set((state) => {
        state.storeUser = {
          ...user,
          tokens: [],
        };
      });
    };

    const addUserTokenInfo = (tokenInfo: StoreTokenInfo) => {
      set((state) => {
        state.storeUser?.tokens.push(tokenInfo);
      });
    };

    const updateUserAvatar = (url: string) => {
      set((state) => {
        if (!state.storeUser) return;

        state.storeUser.avatar = url;
      });
    };

    const clearStore = () => {
      set((state) => {
        state.storeUser = null;
      });
    };

    return {
      storeUser: null,
      setStoreUser,
      addUserTokenInfo,
      updateUserAvatar,
      clearStore,
    };
  },
  { persisted: true }
);

export const useUserStore = () => useStore(userStore);
