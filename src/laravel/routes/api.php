<?php

use App\Http\Controllers\Api\ProductController;
use App\Http\Controllers\Api\UserController;
use Illuminate\Http\Request;
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

    Route::get('/demo', function () {
        return response('Test', 200);
    });
});

