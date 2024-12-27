<script lang="ts">

    let { form } = $props();

</script>

<div class="bg-gray-100 h-screen flex items-center justify-center">
    <div class="bg-white shadow-lg rounded-lg p-8 max-w-sm w-full">
        <form method="POST">
            <h2 class="text-2xl font-bold text-gray-800 text-center mb-4">Login</h2>
            {#if form?.message === "Server error" || form?.message === "Invalid username or password"}
                <h4 class="text-center mb-3 text-md text-red-400">*{form?.message}</h4>
            {/if}
            <div class="mb-4">
                <label for="username" class="block text-gray-700 text-sm font-medium mb-2">Username</label>
                <input 
                    type="text" id="username" name="username" placeholder="Enter your username" 
                    class="w-full px-4 py-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-400"
                    value=""
                >
                {#if form?.usernameError != null}
                    <p class="text-red-400 ml-4">*{form?.usernameError}</p>
                {/if}
            </div>
            <div class="mb-4">
                <label for="password" class="block text-gray-700 text-sm font-medium mb-2">Password</label>
                <input 
                    type="password" 
                    id="password" 
                    name="password"
                    placeholder="Enter your password" 
                    class="w-full px-4 py-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-400"
                    value=""
                >
                {#if form?.passwordError != null}
                    <p class="text-red-400 ml-4">*{form?.passwordError}</p>
                {/if}
            </div>
            <button
                type="submit"
                class="w-full bg-blue-600 text-white py-2 px-4 rounded-lg hover:bg-blue-700 transition duration-300">
                Login
            </button>
        <p class="text-sm text-gray-500 text-center mt-4">
            Don't have an account? <a href="/user/createuser" class="text-blue-600 hover:underline">Sign up</a>
        </p>
        </form>
    </div>
</div>