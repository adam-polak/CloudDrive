<script lang="ts">
    let username = $state("");
    let usernameError = $state("");

    let password = $state("");
    let passwordError = $state("");

    function tryLogin() {
        console.log(username);
        console.log(password);
    }
</script>

<div class="bg-gray-100 h-screen flex items-center justify-center">
    <div class="bg-white shadow-lg rounded-lg p-8 max-w-sm w-full">
        <h2 class="text-2xl font-bold text-gray-800 text-center mb-4">Login</h2>
        <div class="mb-4">
            <label for="username" class="block text-gray-700 text-sm font-medium mb-2">Username</label>
            <input 
                type="text" id="username" placeholder="Enter your username" 
                class="w-full px-4 py-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-400"
                bind:value={username}
            >
            {#if usernameError.length != 0}
                <p class="text-red-400 ml-4">*{usernameError}</p>
            {/if}
        </div>
        <div class="mb-4">
            <label for="password" class="block text-gray-700 text-sm font-medium mb-2">Password</label>
            <input 
                type="password" 
                id="password" 
                placeholder="Enter your password" 
                class="w-full px-4 py-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-400"
                bind:value={password}
            >
            {#if passwordError.length != 0}
                <p class="text-red-400 ml-4">*{usernameError}</p>
            {/if}
        </div>
        <button
            onclick={tryLogin}
            class="w-full bg-blue-600 text-white py-2 px-4 rounded-lg hover:bg-blue-700 transition duration-300">
            Login
        </button>
      <p class="text-sm text-gray-500 text-center mt-4">
        Don't have an account? <a href="/user/createuser" class="text-blue-600 hover:underline">Sign up</a>
      </p>
    </div>
</div>