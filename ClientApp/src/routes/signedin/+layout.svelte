<header
    class="bg-white shadow-md p-4 flex gap-3 justify-between items-center"
  >
    <div>
      <a href="/signedin">
        <h1 class="font-bold text-2xl text-blue-600">CloudDrive</h1>
      </a>
      <a
        href="/signedin/signout"
        class="hover:underline text-sm ml-2 text-black">Sign out</a
      >
    </div>
    <div class="flex gap-2">
      <input
        type="text"
        placeholder="Search folder..."
        class="border rounded-md px-4 py-2 text-sm focus:outline-none focus:ring-2 focus:ring-blue-400"
      />
      <a
        href="signedin/createFolder"
        class="bg-blue-500 text-white px-4 py-2 rounded-md hover:bg-blue-600"
        >Create Folder</a
      >
      <a
        href="signedin/uploadFile"
        class="bg-blue-500 text-white px-4 py-2 rounded-md hover:bg-blue-600"
        >Upload File</a
      >
    </div>
  </header>

<slot />