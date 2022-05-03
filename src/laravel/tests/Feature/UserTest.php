<?php

namespace Tests\Feature;

use App\Models\User;
use Ramsey\Uuid\Uuid;
use Symfony\Component\HttpFoundation\Response;
use Tests\TestCase;

class UserTest extends TestCase
{
    /** @test */
    public function get_user_with_existing_id_returns_200_ok(): void
    {
        $user = User::first();

        $response = $this->get('/api/v1/users/' . $user->id);

        $response->assertStatus(Response::HTTP_OK);
        $this->assertInstanceOf(User::class, $response->original);
    }

    /** @test */
    public function get_user_with_not_existing_id_returns_404_not_found(): void
    {
        $response = $this->get('/api/v1/users/' . Uuid::uuid4());

        $response->assertStatus(Response::HTTP_NOT_FOUND);
    }

    /** @test */
    public function get_user_with_existing_email_returns_user_with_orders(): void
    {
        $user = User::first();

        $response = $this->get('/api/v1/users/email/' . $user->email);

        $response->assertStatus(Response::HTTP_OK);
        $this->assertInstanceOf(User::class, $response->original);
        $this->assertIsArray($response->json('orders'));
    }

    /** @test */
    public function create_user_with_valid_params_returns_201_created(): void
    {
        $user = User::make([
            'name' => 'Test User',
            'email' => 'test@email.com',
            'password' => \Hash::make('password'),
            'phone' => '+4523456789',
        ]);

        $response = $this->post('/api/v1/users', $user->toArray());

        $response->assertStatus(Response::HTTP_CREATED);
        $this->assertInstanceOf(User::class, $response->original);
        $this->assertEquals($user->name, $response->json('name'));
        $this->assertEquals($user->email, $response->json('email'));
    }

    /** @test */
    public function create_user_with_invalid_params_returns_400_bad_request(): void
    {
        $response = $this->post('/api/v1/users', []);

        $response->assertStatus(Response::HTTP_BAD_REQUEST);
    }

    /** @test */
    public function update_user_with_valid_user_returns_204_no_content(): void
    {
        $user = User::first();

        $updatedUser = User::make([
            'name' => 'Updated User',
            'phone' => '+45Updated'
        ]);

        $response = $this->put('/api/v1/users/' . $user->id, $updatedUser->toArray());

        $user = User::find($user->id);

        $response->assertStatus(Response::HTTP_NO_CONTENT);
        $this->assertEquals($user->name, $updatedUser->name);
        $this->assertEquals($user->phone, $updatedUser->phone);
    }

    /** @test */
    public function delete_user_with_existing_id_returns_204_no_content(): void
    {
        $user = User::first();

        $response = $this->delete('/api/v1/users/' . $user->id);

        $response->assertStatus(Response::HTTP_NO_CONTENT);
        $this->assertNull(User::find($user->id));
    }

    /** @test */
    public function delete_user_with_non_existing_id_returns_404_not_found(): void
    {
        $response = $this->delete('/api/v1/users/' . Uuid::uuid4());

        $response->assertStatus(Response::HTTP_NOT_FOUND);
    }
}
