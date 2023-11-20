<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use Illuminate\Routing\Controller as BaseController;
use Illuminate\Support\Str;
use DateTimeImmutable;

use Lcobucci\JWT\Encoding\ChainedFormatter;
use Lcobucci\JWT\Encoding\JoseEncoder;
use Lcobucci\JWT\Signer\Hmac\Sha256;
use Lcobucci\JWT\Signer\Key\InMemory;
use Lcobucci\JWT\Token\Builder;


class Controller extends BaseController
{

    public function getToken(Request $request)
    {
        $secret_id = \config('jwt_config.secret_id');
        $secret_value = \config('jwt_config.secret_value');
        $client_id = \config('jwt_config.client_id');

        $username = $request->query("username");

        $tokenBuilder = (new Builder(new JoseEncoder(), ChainedFormatter::default()));
        $algorithm = new Sha256();
        $signingKey = InMemory::plainText($secret_value);

        $currentTime = new DateTimeImmutable();

        $token = $tokenBuilder
            // Configures the issuer (iss claim)
            ->issuedBy($client_id)

            // Configures the audience (aud claim)
            ->permittedFor('tableau')

            // Configures the id (jti claim)
            ->identifiedBy(Str::uuid())

            // Configures the expiration time of the token (exp claim)
            ->expiresAt($currentTime->modify('+5minutes'))

            // Sub
            ->relatedTo($username)

            // Configures a new claim
            ->withClaim('scp', ["tableau:views:embed", "tableau:views:embed_authoring"])

            // Configures a new header
            ->withHeader('iss', $client_id)
            ->withHeader('kid', $secret_id)

            // Builds a new token
            ->getToken($algorithm, $signingKey);

        return response()->json(['token' => $token->toString()]);
    }
}
