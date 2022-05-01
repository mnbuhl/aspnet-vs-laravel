<?php

namespace App\Http\Requests\Products;

use Illuminate\Foundation\Http\FormRequest;

class GetProductsRequest extends FormRequest
{
    /**
     * Determine if the user is authorized to make this request.
     *
     * @return bool
     */
    public function authorize(): bool
    {
        return true;
    }

    /**
     * Get the validation rules that apply to the request.
     *
     * @return array
     */
    public function rules(): array
    {
        return [
            'pageIndex' => 'integer|min:1',
            'pageSize' => 'integer|min:1|max::50',
            'sort' => 'string|in:name,-name,created_at,-created_at',
            'search' => 'string|max:255',
        ];
    }
}
