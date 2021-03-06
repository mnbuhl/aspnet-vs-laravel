<?php

use App\Http\Controllers\Api\OrderController;
use App\Http\Controllers\Api\ProductController;
use App\Http\Controllers\Api\UserController;
use Illuminate\Support\Facades\Route;

/*
|--------------------------------------------------------------------------
| API Routes
|--------------------------------------------------------------------------
|
| Here is where you can register API routes for your application. These
| routes are loaded by the RouteServiceProvider within a group which
| is assigned the "api" middleware group. Enjoy building your API!
|
*/

Route::prefix('v1')->group(function () {
    Route::controller(UserController::class)->group(function () {
        Route::get('/users/email/{user}', 'showByEmail')->name('users.showByEmail');
        Route::get('/users/{user}', 'show')->name('users.show');
        Route::post('/users', 'store')->name('users.store');
        Route::put('/users/{user}', 'update')->name('users.update');
        Route::delete('/users/{user}', 'destroy')->name('users.destroy');
    });

    Route::resource('products', ProductController::class);

    Route::resource('orders', OrderController::class, ['except' => ['update']]);

    Route::get('/demo', function () {
        return response('Test', 200);
    });

    Route::post('/demo', function () {
        \App\Models\User::truncate();
        \App\Models\Product::truncate();
        \App\Models\Address::truncate();
        \App\Models\ShippingDetails::truncate();

        return response()->json(['success' => true]);
    });
});

