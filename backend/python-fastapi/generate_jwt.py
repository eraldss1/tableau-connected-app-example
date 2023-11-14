import datetime
import os
import uuid

import jwt
from dotenv import load_dotenv

load_dotenv()

secretId = os.getenv("SECRET_ID")
secretValue = os.getenv("SECRET_VALUE")
clientId = os.getenv("CLIENT_ID")
tokenExpiryInMinutes = 5  # Max of 10 minutes.
scopes = ["tableau:views:embed", "tableau:views:embed_authoring"]


def get_payload(username):
    sub = username
    aud = "tableau"
    exp = datetime.datetime.utcnow() + datetime.timedelta(minutes=tokenExpiryInMinutes)
    jti = str(uuid.uuid4())
    scp = scopes

    payload = {
        "iss": clientId,
        "exp": exp,
        "jti": jti,
        "aud": aud,
        "sub": sub,
        "scp": scp,
    }

    return payload


def get_jwt(username):
    payload = get_payload(username)

    token = jwt.encode(
        payload,
        secretValue,
        algorithm="HS256",
        headers={
            "kid": secretId,
            "iss": clientId,
        },
    )

    return token
