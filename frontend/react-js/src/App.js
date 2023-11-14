import { useEffect, useState } from "react";
import { getJwt } from "./api/getJwt";

function App() {
  const [token, setToken] = useState("asd");

  useEffect(() => {
    const result = getJwt("erald.siregar@visidata.co");
    setToken(result);
  }, []);

  return <p>Token : {token && <>{token.toString()}</>}</p>;
}

export default App;
