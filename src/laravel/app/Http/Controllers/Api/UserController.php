<?php

namespace App\Http\Controllers\Api;

use App\Http\Controllers\Controller;
use App\Http\Requests\StoreUserRequest;
use App\Http\Requests\UpdateUserRequest;
use App\Models\User;
use Illuminate\Http\JsonResponse;
use Illuminate\Support\Facades\Log;

class UserController extends Controller
{
    public function store(StoreUserRequest $request): JsonResponse
    {
        $user = User::create($request->validated());

        if (!$user) {
            Log::info("Failed to create user");
            return response()->json(['message' => 'Failed to create user'], 400);
        }

        return response()->json($user, 201);
    }

    public function show(User $user): JsonResponse
    {
        if (!isset($user)) {
            Log::info("User {$user} not found");
            return response()->json(['message' => 'User not found'], 404);
        }

        return response()->json($user);
    }

    public function showByEmail(string $email): JsonResponse
    {
        $user = User::whereEmail($email)->with(['orders', 'orders.orderLines'])->get();

        if (!$user) {
            Log::info("User with email {$email} not found");
            return response()->json(['message' => 'User not found'], 404);
        }

        return response()->json($user);
    }

    public function update(UpdateUserRequest $request, User $user): JsonResponse
    {
        try {
            $user->updateOrFail($request->validated());
        } catch (\Throwable $e) {
            Log::error("Failed to update user", $e->getTrace());
            return response()->json(['message' => 'User not updated'], 400);
        }

        return response()->json(null, 204);
    }

    public function destroy(User $user): JsonResponse
    {
        $user->delete();

        return response()->json(null, 204);
    }
}
