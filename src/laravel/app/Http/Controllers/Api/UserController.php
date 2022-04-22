<?php

namespace App\Http\Controllers\Api;

use App\Http\Controllers\Controller;
use App\Http\Requests\StoreUserRequest;
use App\Http\Requests\UpdateUserRequest;
use App\Models\User;
use Illuminate\Http\JsonResponse;

class UserController extends Controller
{
    public function show(User $user): JsonResponse
    {
        return new JsonResponse($user);
    }

    public function showByEmail(string $email): JsonResponse
    {
        $user = User::whereEmail($email)->with(['orders', 'orders.orderLines'])->get();

        if (!$user) {
            return new JsonResponse(null, 404);
        }

        return new JsonResponse($user);
    }

    public function store(StoreUserRequest $request): JsonResponse
    {
        $user = User::create($request->validated());

        return new JsonResponse($user, 201);
    }

    public function update(UpdateUserRequest $request, User $user): JsonResponse
    {
        $user->update($request->validated());

        return new JsonResponse(null, 204);
    }

    public function destroy(User $user): JsonResponse
    {
        $user->delete();

        return new JsonResponse(null, 204);
    }
}
