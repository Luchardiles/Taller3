import { Stack, useLocalSearchParams } from "expo-router";
import CommitScreen from "../../../../components/commits/CommitScreen";

type ParamsSearch = {
  name: string;
};
const Commit = () => {
  const { name } = useLocalSearchParams<ParamsSearch>();
  return (
    <>
      <Stack.Screen
        options={{
          title: "Información",
          headerShown: true,
          headerBackButtonMenuEnabled: true,
        }}
      />
      <CommitScreen commitName={name} />
    </>
  );
};

export default Commit;
