<?php

namespace App\Http\Middleware;

use Closure;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\ModelNotFoundException;
use Illuminate\Http\JsonResponse;
use Illuminate\Http\RedirectResponse;
use Illuminate\Http\Request;
use Illuminate\Http\Response;
use Illuminate\Support\Reflector;
use Ramsey\Uuid\Uuid;
use Ramsey\Uuid\UuidInterface;

class ValidateUuids
{
    /**
     * Handle an incoming request.
     *
     * @param Request $request
     * @param Closure(\Illuminate\Http\Request): (\Illuminate\Http\Response|\Illuminate\Http\RedirectResponse)  $next
     * @return JsonResponse
     */
    public function handle(Request $request, Closure $next)
    {
        $route = $request->route();

        $parameters = array_filter($route->signatureParameters(), function ($param) {
            return is_subclass_of(Reflector::getParameterClassName($param), Model::class);
        });

        foreach ($parameters as $parameter) {
            $uuid = $route->parameter($parameter->name);

            if (!Uuid::isValid($uuid)) {
                return new JsonResponse(null, 404);
            }

            $route->setParameter($parameter->name, Uuid::fromString($uuid));
        }

        return $next($request);
    }
}
