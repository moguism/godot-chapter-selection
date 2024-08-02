extends CanvasLayer

var chapter_select = load("res://GoToChapterSelect.cs")
var chapter_select_node = chapter_select.new()

func _on_button_pressed():
	chapter_select_node.ReturnToChapters()
	get_tree().quit()
