<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use Illuminate\Routing\Controller as BaseController;

class Controller extends BaseController
{

    public function getToken(Request $request)
    {
        $username = $request->query("username");
        return $username;
    }
}
