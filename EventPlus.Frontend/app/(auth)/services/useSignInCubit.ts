import auth from "@react-native-firebase/auth";
import { GoogleSignin } from "@react-native-google-signin/google-signin";
import { router } from "expo-router";
import { useState } from "react";
import { authenticate, checkIfRegistered } from "~/api/jwt/jwt.api";
import { useUserStore } from "~/store/user/user.store";
import { useAuthStore } from "../state/auth.store";

export const useSignInCubit = () => {
  const { setProviderUser } = useAuthStore();
  const { setStoreUser, addUserTokenInfo } = useUserStore();
  const [isLoading, setIsLoading] = useState<boolean>(false);

  const loginByGoogle = async () => {
    // Check if your device supports Google Play
    await GoogleSignin.hasPlayServices({ showPlayServicesUpdateDialog: true });
    // Get the users ID token
    const { idToken } = await GoogleSignin.signIn();

    // Create a Google credential with the token
    const googleCredential = auth.GoogleAuthProvider.credential(idToken);

    // Sign-in the user with the credential
    const { user } = await auth().signInWithCredential(googleCredential);

    return user;
  };

  const signOn = async () => {
    try {
      setIsLoading(true);
      const googleUser = await loginByGoogle();

      const { isProviderRegistered } = await checkIfRegistered({
        key: googleUser.uid,
        provider: "google",
      });

      if (!isProviderRegistered) {
        setProviderUser(googleUser);

        if (!isProviderRegistered) router.push("/set-up-profile");
        return;
      }

      const user = await authenticate({
        type: "login",
        provider: "google",
        providerKey: googleUser.uid,
        email: googleUser.email,
        providerMetadata: {
          token: await googleUser.getIdToken(),
        },
      });

      setStoreUser({
        firstName: user.firstName,
        lastName: user.lastName,
        username: user.username,
        avatar: user.avatar,
        commands: user.commands,
      });

      addUserTokenInfo({
        token: user.token,
        tokenExpires: user.tokenExpires,
        refreshToken: user.refreshToken,
        refreshTokenExpires: user.refreshTokenExpires,
      });

      router.replace("home");
    } finally {
      setIsLoading(false);
    }
  };

  return {
    signOn,
    signOnIsLoading: isLoading,
  };
};
