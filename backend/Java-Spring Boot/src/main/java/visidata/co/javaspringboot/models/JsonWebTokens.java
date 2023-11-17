package visidata.co.javaspringboot.models;

import com.nimbusds.jose.*;
import com.nimbusds.jose.crypto.MACSigner;
import com.nimbusds.jwt.JWTClaimsSet;
import com.nimbusds.jwt.SignedJWT;
import io.github.cdimascio.dotenv.Dotenv;
import lombok.Getter;

import java.util.Calendar;
import java.util.Date;
import java.util.UUID;

public class JsonWebTokens {
    @Getter
    private String token;
    private final String clientId;
    private final String secretId;
    private final String secretValue;
    private final String username;
    private final int expiryInMinutes;

    public JsonWebTokens(String username) {
        Dotenv dotenv = Dotenv.load();

        this.clientId = dotenv.get("CLIENT_ID");
        this.secretId = dotenv.get("SECRET_ID");
        this.secretValue = dotenv.get("SECRET_VALUE");
        this.username = username;
        this.expiryInMinutes = 5;

        this.generateJWT();
    }

    public void setToken(String token) {
        this.token = token;
    }

    private void generateJWT() {
        Calendar expiry = Calendar.getInstance();
        expiry.add(Calendar.MINUTE, this.expiryInMinutes);

        String[] scopes = {"tableau:views:embed", "tableau:views:embed_authoring"};

        UUID myGeneratedId = UUID.randomUUID();

        JWTClaimsSet claimsSet = new JWTClaimsSet
                .Builder()
                .audience("tableau")
                .subject(this.username)
                .expirationTime(expiry.getTime())
                .issuer(this.clientId)
                .issueTime(new Date())
                .jwtID(myGeneratedId.toString())
                .claim("scp", scopes)
                .build();

        @SuppressWarnings("ReassignedVariable") String JWTValue;
        try {
            JWSSigner signer = new MACSigner(secretValue);
            JWSHeader header = new JWSHeader
                    .Builder(JWSAlgorithm.HS256)
                    .keyID(secretId)
                    .type(JOSEObjectType.JWT)
                    .customParam("iss", clientId)
                    .build();

            SignedJWT signedJWT = new SignedJWT(header, claimsSet);
            signedJWT.sign(signer);
            JWTValue = signedJWT.serialize();

        } catch (JOSEException jEx) {
            System.out.println("JWT signing failed: ");
            //noinspection ThrowablePrintedToSystemOut
            System.out.println(jEx);
            JWTValue = jEx.getCause().getClass().getSimpleName();
        }

        this.setToken(JWTValue);
    }
}
