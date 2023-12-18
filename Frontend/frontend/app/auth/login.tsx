import LoginScreen from "../../components/auth/LoginScreen";
import { Stack } from "expo-router";
import { useSelector } from "react-redux";
import { selectEmail } from "../../store/userSlice";
import { useRouter } from "expo-router";
import React from "react";
import { SafeAreaView } from "react-native-safe-area-context";
import { ActivityIndicator, Text } from "react-native-paper";
import { StyleSheet } from "react-native";

const Login = () => {
  const [isLoading, setIsLoading] = React.useState<boolean>(false);
  const router = useRouter();
  const email = useSelector(selectEmail);
  React.useEffect(() => {
    if (email) {
      setIsLoading(true);
      setTimeout(() => {
        router.push("/(drawer)/repos/repository");
      }, 2000);
    }
  }, []);

  if (isLoading)
    return (
      <SafeAreaView style={styles.container}>
        <Text variant="displaySmall">Iniciando sesión...</Text>
        <ActivityIndicator animating={true} size={"large"} />
      </SafeAreaView>
    );

  return (
    <>
      <Stack.Screen options={{ headerShown: true, title: "Iniciar sesión" }} />
      <LoginScreen />
    </>
  );
};

export default Login;

const styles = StyleSheet.create({
  container: {
    flex: 1,
    padding: 20,
    alignItems: "center",
    gap: 20,
  },
});
