   M S B A M L   `   `   `  ���� N  JInstagram Assistant, Version=1.0.6.7, Culture=neutral, PublicKeyToken=nullC&clr-namespace:Instagram_Assistant.ViewInstagram_Assistant.View  2    ,Instagram_Assistant.View.GeoAudiencePageView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     8local&clr-namespace:Instagram_Assistant.View     _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors    � 	Resources     	FlashAnim �� 
TargetName �� TargetProperty W� From	 W� To g� AutoReverse g� Duration g� RepeatBehavior  	ColorAnim  SecondColor	 �� To��%�   &	        &	 [     �� .��W� $ Status��$ Opacity�$ 1X�$	 0.3X� . $ 0:0:1D�$ Forever���� .���� $ Status��$( "Foreground.(SolidColorBrush.Color)�$ Forever�� . $ 0:0:1D�# �  .��� 	 � RowDefinitions	 �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� 
 � ColumnDefinitions
 �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� .+��� $��21��$��6��$��1��$��0��  TextBlockMainStyle#���  $��Center=�$��Center=�  H1#���  
Overview�� $��0��$��9��$��2��$��13�� �� CornerRadius$ 3��  ThirdColorBrush#��[  J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     
BlurRadius$ 20Q�   ShadowDepth$	 0.9Q�  
FirstColor   Color# [ �� $��1��$��9��$��9��.+��� #���  $��0,0,0,5q�
Geo list�� $��0,4,0,0q�$	��330��$	��250�� �� AcceptsReturn . $��16(�  MainFont#���  
������ 
�������$��0q�
 � Tag$ instagram
ilonmask
shrek�� �� TextWrapping$
 Wrap=����� *CollectFrom+ �� UpdateSourceTrigger$ PropertyChanged=�.���� $��2��$��22��$��4��$��15��$��Center=� �� Orientation$ Vertical=�.+���   MainButtonStyle#���  $��40������ *StartAudienceCommand+���� *ButtonContent+�� -    ��Name$ Status��$��Center=�
�������$��0,20,0,5q� 
	 Bigger#��� 	  
 TextBlockThinStyle#��� 
 ���� *Stats.Status+*  $Microsoft.Xaml.Behaviors.Interaction   Triggers /  )Microsoft.Xaml.Behaviors.Core.DataTrigger     Value$ WORKING��:  4Microsoft.Xaml.Behaviors.Core.PropertyChangedTrigger   Binding �� *Stats.Status+*  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. <  6Microsoft.Xaml.Behaviors.Media.ControlStoryboardAction  5  /Microsoft.Xaml.Behaviors.Media.StoryboardAction   
Storyboard# [    ControlStoryboardOption$
 Play=�  $	 OFF�� �� *Stats.Status+.   # [ $
 Stop=��� #���  $��Center=�$��
ExtraLight"�$��0,10,0,0q�$��Technical account���� $��Center=�
�������$��15(�$��0,0,0,5q�$��
ExtraLight"����� *Stats.TechAccount+�� #���  $��Center=�$��
ExtraLight"�$��0,10,0,0q�$��Geoposition���� $��Center=�
�������$��13(�$��0,0,0,5q�$��
ExtraLight"����� *Stats.Competitor+�� #���  $��Center=�$��
ExtraLight"�$��0,10,0,0q�$��Audience count���� $��Center=�
�������$��15(�$��0,0,0,5q�$��
ExtraLight"����� *Stats.Count+�� $��Target��#���  ��   ComboBoxStyle#���   �� SelectedIndex �� *ComboBoxSelectedIndex+{��� *ComboBoxEnable+.@��� $��Recent medias���� $��
Top medias���� $��Stories��{�.  �� $��Center=�$��0,50,0,0q�.+��� $��Center=�$��0,0,0,5q�
�������#��� 	   TextBlockExtraThinStyle#���  ���� *Stats.TimeInWork+�� $��Center=�#��� 
 $��Time in work���� $ Vertical=�$��16��$��2��$��0��$��20��.+��� #��� 
 $��0,5,0,0q����� *AudienceProcess.Message+�� $ 
Horizontal=�$��Center=�.+��  � Maximum$	 100Q� � Minimum$ 0Q�$��10��$	��530��  SecondColorBrush#���   � Value �� *AudienceProcess.Percent+�� $��35��$��5,0,0,3q�#���  ���� *AudienceProcess.Percent+  �� StringFormat$  {0:0}%��