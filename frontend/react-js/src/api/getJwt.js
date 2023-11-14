import { endpoint } from "./endpoint";

async function getJwt(username) {
  const url =
    endpoint +
    "token?" +
    new URLSearchParams({
      username: username,
    });

  const result = await fetch(url)
    .then((response) => response.json())
    .then((data) => data.token);
  console.log(result);

  return result;
}

export { getJwt };
