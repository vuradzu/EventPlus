import auth from "@react-native-firebase/auth";
import { GoogleSignin } from "@react-native-google-signin/google-signin";
import { router } from "expo-router";
import { useState } from "react";
import { _checkIfRegistered } from "~/api/jwt/jwt.api";
import { JwtHelper } from "~/utils/helpers/jwtHelper";
import { useAuthStore } from "../state/auth.store";

export const useSignInCubit = () => {
  const { setProviderUser } = useAuthStore();
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

      const { isProviderRegistered } = await _checkIfRegistered({
        key: googleUser.uid,
        provider: "google",
      });

      // registration
      if (!isProviderRegistered) {
        setProviderUser(googleUser);

        if (!isProviderRegistered) router.push("/set-up-profile");
        return;
      }

      // login
      const jwtResult = await JwtHelper.authenticateUser({
        type: "login",
        provider: "google",
        providerKey: googleUser.uid,
        email: googleUser.email,
        providerMetadata: {
          token: await googleUser.getIdToken(),
        },
      });

      if (!!jwtResult.lastActivityCommand) {
        router.replace("home/home");
        return;
      }

      router.push("command-onboarding");
    } finally {
      setIsLoading(false);
    }
  };

  return {
    signOn,
    signOnIsLoading: isLoading,
  };
};
