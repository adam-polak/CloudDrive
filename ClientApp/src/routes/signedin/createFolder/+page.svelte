<script lang="ts">
    
</script>

<div class="bg-gray-100 min-h-screen flex flex-col">
    <!-- Header -->
    <header class="bg-white shadow-md p-4 flex gap-3 justify-between items-center">
        <div>
            <h1 class="font-bold text-2xl text-blue-600">CloudDrive</h1>
            <a href="/signedin/signout" class="hover:underline text-sm ml-2 text-black">Sign out</a>
        </div>
    </header>
    <main>
        <div class="text-center flex-col p-4 mx-auto mt-10 w-96 h-[60vh] rounded-md shadow bg-white">
            <form>
                <div>
                    <div> 
                        <h1 class="text-2xl">Create Folder</h1>
                    </div>
                </div>
               <div class="mt-4">
                    <label for="foldername">Folder Name:</label>
                    <input class="shadow rounded-md pl-2" type="text" id="foldername" name="foldername" placeholder="Enter folder name..." />
               </div> 
            </form>
            <div class="ml-[-60%] mt-72">
                <a href="/signedin" class="bg-red-400 text-white text-l px-5 py-3 rounded-md hover:bg-red-500">Cancel</a>
            </div>
        </div>
    </main>
</div>
