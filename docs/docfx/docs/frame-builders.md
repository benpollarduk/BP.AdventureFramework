# Overview
In BP.AdventureFramework output is handled using the **FrameBuilders**. A FrameBuilder is essentially a class that builds a **Frame** that can render a specific state in the game. This **Frame** can then be rendered on a **TextWriter** by calling its **Render** method. Think of the **FrameBuilder** as the instructions that build the output display and the **Frame** as the output itself.

There are a few types of **FrameBuilder**, each responsible for rendering a specific game state.
* **SceneFrameBuilder** is responsible for building frames that render the scenes in a game.
* **TitleFrameBuilder** is responsible for building the title screen frame.
* **RegionMapFrameBuilder** is responsible for building a frame that displays a map of a Region.
* **TransitionFrameBuilder** is responsible for building frames that display transitions.
* **AboutFrameBuilder** is responsible for building a frame to display the about information.
* **HelpFrameBuilder** is responsible for building frames to display the help.
* **GameOverFrameBuilder** is responsible for building a frame to display the game over screen.
* **CompletionFrameBuilder** is responsible for building a frame to display the completion screen.
* **ConversationFrameBuilder** is responsible for building a frame that can render a conversation.

A game accepts a **FrameBuilderCollection**. A **FrameBuilderCollection** is a collection of all of the different **FrameBuilders** required to render a game. All **FrameBuilders** are extensible, so the output for all parts of the game can be fully customised.
