Sorry, the answer is "NO". Web Screenshot uses WIN32 API to draw image. That means it copies image from its window in your computer. If you run it as an unlogged-in user, it doesn't have a displayed window, and so it can't draw the web site image for you.
